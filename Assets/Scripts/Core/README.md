# Core

## Spacial Gravity

**CircleGravity2D** is a MonoBehvaiour that creates a region where gravity points to the center of a circular region.  Any GameObject that has a **SpacialGravityReceiver** component will accelerate in the direction of this gravity field when inside the circle.  Gravity can scale with the inverse square, inverse, or not at all with the distance from the center of the circle to the origin of the SpacialGravityReceiver's GameObject; this scaling can be set in Project Settings>Spacial Gravity.
