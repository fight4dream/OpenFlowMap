using UnityEngine;

[CreateAssetMenu(fileName = "BlurEffect", menuName = "OpenFlowmap/Processor/BlurEffect")]
public class BlurEffect : RayProcessor
{
    [SerializeField, Range(0, 5)] int m_blurSize = 1;
    internal override void Execute(RayProjector rayProjector)
    {
        if (m_blurSize == 0)
        {
            return;
        }
        var rays = rayProjector.GetRays();
        int width = rayProjector.RayCount;
        BlurRays(rays, width, m_blurSize);
    }

    void BlurRays(Ray[] rays, int width, int size)
    {
        Ray[] outputRays = new Ray[rays.Length];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                var rayIndex = x * width + y;
                var ray = rays[rayIndex];
                var averageDirection = Vector3.zero;
                int count = 0;

                for (int i = -size; i <= size; i++)
                {
                    for (int j = -size; j <= size; j++)
                    {
                        int xPos = Mathf.Clamp(x + i, 0, width - 1);
                        int yPos = Mathf.Clamp(y + j, 0, width - 1);

                        averageDirection += rays[xPos * width + yPos].direction;
                        count++;
                    }
                }

                averageDirection /= count;
                var newRay = new Ray(ray.origin, averageDirection);
                outputRays[rayIndex] = newRay;
            }
        }
        for (int i = 0; i < rays.Length; i++)
        {
            rays[i] = outputRays[i];
        }
    }
}
