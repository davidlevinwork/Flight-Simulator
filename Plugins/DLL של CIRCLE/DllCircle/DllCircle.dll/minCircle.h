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












//vector<Point>makeVectorPoints(Point**& points, size_t size) {
//    vector<Point> pointsVec;
//    for (int i = 0; i < size; i++) {
//        Point p(points[i]->x, points[i]->y);
//        pointsVec.push_back(p);
//    }
//    return pointsVec;
//}
//
///// the function calculates the distance between two points.
//float calculateDistance(const Point& p1, const Point& p2) {
//    return sqrtf((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
//}
//
///// the function creates circle from 2 points.
//Circle createCircleByTwoPoints(const Point& p1, const Point& p2) {
//    float xCenter = (p1.x + p2.x) / 2.0f;
//    float yCenter = (p1.y + p2.y) / 2.0f;
//    Point center = Point(xCenter, yCenter);
//    float distance = calculateDistance(p1, p2);
//    float r = distance / 2.0f; /// radius of circle
//    return { center, r };
//}
//
///// the function checks if all the points are in the circle or on the boundary. if not - returns false.
//bool ifAllPointsInCircle(const Circle& tempCircle, const vector<Point>& perimeterVec, size_t size) {
//    float r = tempCircle.radius; /// get the radius.
//    for (int i = 0; i < size; i++) { /// check if distance from point to center circle is bigger than radius.
//        if (calculateDistance(tempCircle.center, perimeterVec[i]) > r)
//            return false;
//    }
//    return true;
//}
//
///// the function handle the case that the x values of 2 points are equals, and find their relative circle.
//Circle specialCaseThreePointsXcases(const Point& p1, const Point& p2, const Point& p3) {
//    float x12 = (p1.x + p2.x) / 2.0f, y12 = (p1.y + p2.y) / 2.0f, x23 = (p2.x + p3.x) / 2.0f,
//        y23 = (p2.y + p3.y) / 2.0f, x13 = (p1.x + p3.x) / 2.0f; /// middle of the x and y values points.
//    float m23;
//    float opoM12, opoM23; /// opposite slopes of M12, M23
//    float xCenter, yCenter;
//    float radius;
//    opoM12 = 0; /// the opposite slope is zero.
//    yCenter = y12; /// in this case the y center is the y values of middle 1 and 2.
//    if (p2.y == p3.y) { /// check if the p2y, p3y are equals, handle this case.
//        xCenter = x23;
//        radius = calculateDistance(Point(xCenter, yCenter), p1);
//        return { Point(xCenter, yCenter), radius };
//    }
//    else if (p1.y == p3.y) { /// check if the p1y, p3y are equals, handle this case.
//        xCenter = x13;
//        radius = calculateDistance(Point(xCenter, yCenter), p1);
//        return { Point(xCenter, yCenter), radius };
//    } /// Base case - and no option for divide in zero
//    m23 = (p2.y - p3.y) / (p2.x - p3.x);
//    opoM23 = -1 / m23;
//    xCenter = ((y23 - y12) + (opoM12 * x12) - (opoM23 * x23)) / (opoM12 - opoM23);
//    Point center(xCenter, yCenter);
//    radius = calculateDistance(center, p1);
//    return { center, radius };
//}
//
///// the function handle the case that the y values of 2 points are equals, and find their relative circle.
//Circle specialCaseThreePointsYcases(const Point& p1, const Point& p2, const Point& p3) {
//    float x12 = (p1.x + p2.x) / 2.0f, x23 = (p2.x + p3.x) / 2.0f, y23 = (p2.y + p3.y) / 2.0f; /// middle of the x and y
//    ///values points.
//    float m23; /// slope of point 2 and 3.
//    float opoM23; /// the opposite slopes.
//    float xCenter, yCenter; /// the center of circle.
//    float radius;
//    xCenter = x12; /// in this case the x center is the x values of middle 1 and 2.
//    m23 = (p2.y - p3.y) / (p2.x - p3.x); /// we check already for not divide in 0.
//    opoM23 = -1 / m23;
//    yCenter = opoM23 * (xCenter - x23) + y23; /// calculate the y value center with equation line.
//    Point center(xCenter, yCenter);
//    radius = calculateDistance(center, p1);
//    return { center, radius };
//}
//
///// the function creates circle from 3 points.
//Circle createCircleByThreePoints(const Point& p1, const Point& p2, const Point& p3) {
//    float x12 = (p1.x + p2.x) / 2.0f; /// middle of x values of point 1 and 2
//    float y12 = (p1.y + p2.y) / 2.0f; /// middle of y values of point 1 and 2
//    float x23 = (p2.x + p3.x) / 2.0f; /// middle of x values of point 2 and 3
//    float y23 = (p2.y + p3.y) / 2.0f; /// middle of y values of point 2 and 3
//    float m12, m23; /// the slope of the lines.
//    float opoM12, opoM23; /// the opposite slopes.
//    float xCenter, yCenter; /// the center of circle.
//    float radius;
//
//    /// check for special cases for x and y values that equals, for not divide in zero.
//    if (p1.x == p2.x) /// case of p1x,p2x are equals
//        return specialCaseThreePointsXcases(p1, p2, p3);
//    else if (p2.x == p3.x) /// case of p2x,p3x are equals
//        return specialCaseThreePointsXcases(p2, p3, p1);
//    else if (p1.x == p3.x) /// case of p1x,p3x are equals
//        return specialCaseThreePointsXcases(p1, p3, p2);
//    else if (p1.y == p2.y) /// case of p1y,p2y are equals
//        return specialCaseThreePointsYcases(p1, p2, p3);
//    else if (p2.y == p3.y) /// case of p2y,p3y are equals
//        return specialCaseThreePointsYcases(p2, p3, p1);
//    else if (p1.y == p3.y) /// case of p1y,p3y are equals
//        return specialCaseThreePointsYcases(p1, p3, p2);
//    /// Base case - calculating 2 equations lines of the 3 points and finally find the circle.
//    m12 = (p1.y - p2.y) / (p1.x - p2.x); /// no chance for divide in zero.
//    m23 = (p2.y - p3.y) / (p2.x - p3.x);
//    opoM12 = -1 / m12;
//    opoM23 = -1 / m23;
//    xCenter = ((y23 - y12) + (opoM12 * x12) - (opoM23 * x23)) / (opoM12 - opoM23);
//    yCenter = opoM12 * (xCenter - x12) + y12;
//    Point center(xCenter, yCenter); /// center of circle.
//    radius = calculateDistance(center, p1);
//    return { center, radius };
//}
//
///// the function calculate minimum enclosing circle with 3 points (maximum - may be less then 3) we get in perimeterVec.
//Circle calculateCircle(vector<Point>& perimeterVec, size_t size) {
//    if (size > 3) { /// too much points.
//        cerr << "Too much points in perimeter vector.";
//        exit(1);
//    }
//    else if (size == 0) { /// case of no points.
//        return { Point(0,0),0 };
//    }
//    else if (size == 1) { /// case of 1 point, the point is center.
//        return { perimeterVec[0],0 };
//    }
//    else if (size == 2) { /// case of 2 points, the center is the middle of 2 points.
//        return createCircleByTwoPoints(perimeterVec[0], perimeterVec[1]);
//    }
//    for (int i = 0; i < size; i++) { /// in case of 3 points we need to check if we can create the circle from 2 points.
//        for (int j = i + 1; j < size; j++) {
//            Circle tempCircle = createCircleByTwoPoints(perimeterVec[i], perimeterVec[j]);
//            if (ifAllPointsInCircle(tempCircle, perimeterVec, size)) { /// check if all the 3 points in the circle.
//                return tempCircle;
//            }
//        }
//    }
//    return createCircleByThreePoints(perimeterVec[0], perimeterVec[1], perimeterVec[2]); /// create circle
//    /// from 3 point.
//}
//
///// the function gets to points and swap them
//void swapPoints(Point& p1, Point& p2) {
//    Point temp = p1;
//    p1 = p2;
//    p2 = temp;
//}
//
///// the function returns the minimum enclosing circle of the points we get.
//Circle recMinCircle(vector<Point>& pVec, vector<Point> perimeterVec, size_t size) {
//    if (size == 0 || perimeterVec.size() == 3) { /// if 0: no more points to check, if 3: 3 points to calculate circle
//        return calculateCircle(perimeterVec, perimeterVec.size());
//    }
//    size_t num = rand() % size; /// get random point.
//    Point p = pVec[num];
//    swapPoints(pVec[num], pVec[size - 1]); /// swap the random point with the last point in vector.
//
//    Circle recursiveC = recMinCircle(pVec, perimeterVec, size - 1); /// get the circle without the random point
//    if (calculateDistance(p, recursiveC.center) < recursiveC.radius) { /// if p point in the circle.
//        return recursiveC;
//    }
//    perimeterVec.push_back(p); /// p point is not in the circle, so it must be on the boundary - use perimeterVec.
//    return recMinCircle(pVec, perimeterVec, size - 1);
//}
//
///// the function gets array of points and size and returns the minimum enclosing circle by using recMinCircle method
//Circle findMinCircle(Point** points, size_t size) {
//    vector<Point> pVec = makeVectorPoints(points, size); /// enter all points to vector.
//    vector<Point> onPerimeterVec; /// vector that hold the points on the circle boundary.
//    return recMinCircle(pVec, onPerimeterVec, size); /// recursive function return min circle.
//}
//
///// the function gets two float vectors and size and returns the minimum enclosing circle by using recMinCircle method
//Circle findMinCircle(const vector<float>& px, const vector<float>& py, size_t size) {
//    vector<Point> pVec, onPerimeterVec;  /// onPerimeterVec - vector that will hold the points on the circle boundary.
//    for (int i = 0; i < size; i++) {
//        Point p(px[i], py[i]);
//        pVec.push_back(p);
//    }
//    return recMinCircle(pVec, onPerimeterVec, size); /// recursive function return min circle.
//}
//
///// the function return true if the point isn't in the circle. false otherwise.
//bool isPointNotInCircle(const Circle& tempCircle, Point p) {
//    float r = tempCircle.radius; /// get the radius.
//    if (calculateDistance(tempCircle.center, p) > r) /// the point is not in circle
//        return true;
//    return false;
//}
