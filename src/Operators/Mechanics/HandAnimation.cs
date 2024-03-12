using System;
using System.Collections.Generic;

namespace DuckGame.R6S
{
    public class HandAnimation
    {
        public List<float> frameHand1Length = new List<float>();
        public List<float> frameHand2Length = new List<float>();
        public List<Vec2> hand1Offset = new List<Vec2>();
        public List<Vec2> hand2Offset = new List<Vec2>();
        public List<float> hand1Angle = new List<float>();
        public List<float> hand2Angle = new List<float>();

        public int currentFrame1;
        public int currentFrame2;
        public float currentAnimationTime1;
        public float currentAnimationTime2;
        public bool pause;

        public void AddHand1KeyFrame(float length, float angle, Vec2 offset)
        {
            frameHand1Length.Add(length);
            hand1Offset.Add(offset);
            hand1Angle.Add(angle);
        }
        public void AddHand2KeyFrame(float length, float angle, Vec2 offset)
        {
            frameHand2Length.Add(length);
            hand2Offset.Add(offset);
            hand2Angle.Add(angle);
        }

        public void Reset()
        {
            currentAnimationTime1 = 0;
            currentAnimationTime2 = 0;
            currentFrame1 = 0;
            currentFrame2 = 0;
        }

        public void Update()
        {
            currentAnimationTime1++;
            currentAnimationTime2++;
            if (currentFrame1 < frameHand1Length.Count && currentAnimationTime1 >= frameHand1Length[currentFrame1])
            {
                currentFrame1++;
            }
            if (currentFrame2 < frameHand2Length.Count && currentAnimationTime2 >= frameHand2Length[currentFrame2])
            {
                currentFrame2++;
            }
        }

        public Vec2 GetHand1Position()
        {
            if(hand1Offset.Count > currentFrame1)
            {
                return hand1Offset[currentFrame1];
            }
            return new Vec2();
        }
        public Vec2 GetHand2Position()
        {
            if (hand2Offset.Count > currentFrame2)
            {
                return hand2Offset[currentFrame2];
            }
            return new Vec2();
        }
        public float GetHand1Angle()
        {
            if (hand1Angle.Count > currentFrame1)
            {
                return hand1Angle[currentFrame1];
            }
            return 0;
        }
        public float GetHand2Angle()
        {
            if (hand2Angle.Count > currentFrame2)
            {
                return hand2Angle[currentFrame2];
            }
            return 0;
        }
    }
}
