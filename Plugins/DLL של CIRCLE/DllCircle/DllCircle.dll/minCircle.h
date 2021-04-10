#pragma once
#include <iostream>
#include <algorithm>
#include <vector>
#include <stdlib.h>     /* srand, rand */
#include <time.h>       /* time */
#include "anomaly_detection_util.h"

using namespace std;

class Circle {
public:
    Point center;
    float radius;
    Circle(Point c, float r) :center(c), radius(r) {}
    Circle() :center(0, 0), radius(0) {}
};
// --------------------------------------

/// the function gets array of points and makes vector of points from it.
vector<Point>makeVectorPoints(Point**& points, size_t size) {
    vector<Point> pointsVec;
    for (int i = 0; i < size; i++) {
        Point p(points[i]->x, points[i]->y);
        pointsVec.push_back(p);
    }
    return pointsVec;
}

/// the function calculates the distance between two points.
float dist(const Point& p1, const Point& p2) {
    return sqrtf((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
}

/// the function creates circle from 2 points.
Circle createCircleByTwoPoints(const Point& a, const Point& b) {
    float x = (a.x + b.x) / 2;
    float y = (a.y + b.y) / 2;
    float r = dist(a, b) / 2;
    return Circle(Point(x, y), r);
}

/// the function checks if all the points are in the circle or on the boundary. if not - returns false.
bool ifAllPointsInCircle(const Circle& tempCircle, const vector<Point>& perimeterVec, size_t size) {
    float r = tempCircle.radius; /// get the radius.
    for (int i = 0; i < size; i++) { /// check if distance from point to center circle is bigger than radius.
        if (dist(tempCircle.center, perimeterVec[i]) > r)
            return false;
    }
    return true;
}


/// the function creates circle from 3 points.
Circle createCircleByThreePoints(const Point& a, const Point& b, const Point& c) {
    Point mAB((a.x + b.x) / 2, (a.y + b.y) / 2); // mid point of line AB
    float slopAB = (b.y - a.y) / (b.x - a.x); // the slop of AB
    float pSlopAB = -1 / slopAB; // the perpendicular slop of AB

    Point mBC((b.x + c.x) / 2, (b.y + c.y) / 2); // mid point of line BC
    float slopBC = (c.y - b.y) / (c.x - b.x); // the slop of BC
    float pSlopBC = -1 / slopBC; // the perpendicular slop of BC

    float x = (-pSlopBC * mBC.x + mBC.y + pSlopAB * mAB.x - mAB.y) / (pSlopAB - pSlopBC);
    float y = pSlopAB * (x - mAB.x) + mAB.y;
    Point center(x, y);
    float R = dist(center, a);

    return Circle(center, R);
}

/// the function calculate minimum enclosing circle with 3 points (maximum - may be less then 3) we get in perimeterVec.
Circle calculateCircle(vector<Point>& P, size_t size) {
    if (P.size() == 0)
        return Circle(Point(0, 0), 0);
    else if (P.size() == 1)
        return Circle(P[0], 0);
    else if (P.size() == 2)
        return createCircleByTwoPoints(P[0], P[1]);

    // maybe 2 of the points define a small circle that contains the 3ed point
    Circle c = createCircleByTwoPoints(P[0], P[1]);
    if (dist(P[2], c.center) <= c.radius)
        return c;
    c = createCircleByTwoPoints(P[0], P[2]);
    if (dist(P[1], c.center) <= c.radius)
        return c;
    c = createCircleByTwoPoints(P[1], P[2]);
    if (dist(P[0], c.center) <= c.radius)
        return c;
    // else find the unique circle from 3 points
    return createCircleByThreePoints(P[0], P[1], P[2]);
}

/// the function gets to points and swap them
void swapPoints(Point& p1, Point& p2) {
    Point temp = p1;
    p1 = p2;
    p2 = temp;
}

/// the function returns the minimum enclosing circle of the points we get.
Circle recMinCircle(vector<Point>& pVec, vector<Point> perimeterVec, size_t size) {
    if (size == 0 || perimeterVec.size() == 3) { /// if 0: no more points to check, if 3: 3 points to calculate circle
        return calculateCircle(perimeterVec, perimeterVec.size());
    }
    size_t num = rand() % size; /// get random point.
    Point p = pVec[num];
    swapPoints(pVec[num], pVec[size - 1]); /// swap the random point with the last point in vector.

    Circle recursiveC = recMinCircle(pVec, perimeterVec, size - 1); /// get the circle without the random point
    if (dist(p, recursiveC.center) < recursiveC.radius) { /// if p point in the circle.
        return recursiveC;
    }
    perimeterVec.push_back(p); /// p point is not in the circle, so it must be on the boundary - use perimeterVec.
    return recMinCircle(pVec, perimeterVec, size - 1);
}

/// the function gets array of points and size and returns the minimum enclosing circle by using recMinCircle method
Circle findMinCircle(Point** points, size_t size) {
    vector<Point> pVec = makeVectorPoints(points, size); /// enter all points to vector.
    vector<Point> onPerimeterVec; /// vector that hold the points on the circle boundary.
    return recMinCircle(pVec, onPerimeterVec, size); /// recursive function return min circle.
}

/// the function gets two float vectors and size and returns the minimum enclosing circle by using recMinCircle method
Circle findMinCircle(const vector<float>& px, const vector<float>& py, size_t size) {
    vector<Point> pVec, onPerimeterVec;  /// onPerimeterVec - vector that will hold the points on the circle boundary.
    for (int i = 0; i < size; i++) {
        Point p(px[i], py[i]);
        pVec.push_back(p);
    }
    return recMinCircle(pVec, onPerimeterVec, size); /// recursive function return min circle.
}

/// the function return true if the point isn't in the circle. false otherwise.
bool isPointNotInCircle(const Circle& tempCircle, Point p) {
    float r = tempCircle.radius; /// get the radius.
    if (dist(tempCircle.center, p) > r) /// the point is not in circle
        return true;
    return false;
}

