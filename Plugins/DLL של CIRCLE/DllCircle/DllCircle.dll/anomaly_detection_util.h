#pragma once
#include <iostream>
#include <cmath>
using namespace std;



class Line {
public:
    float a, b;
    Line() :a(0), b(0) {};
    Line(float a, float b) :a(a), b(b) {}
    float f(float x) {
        return a * x + b;
    }
};

class Point {
public:
    float x, y;
    Point(float x, float y) :x(x), y(y) {}
};



/// returns the average of X
float avg(float* x, int size) {
    float sum = 0;
    for (int i = 0; i < size; i++)
        sum += x[i];
    float u = (sum / (float)size);
    return u;
}

/// returns the variance of X and Y
float var(float* x, int size) {
    float sum = 0;
    float u = avg(x, size);
    for (int i = 0; i < size; i++)
        sum += powf(x[i], 2.0f);
    sum = sum / (float)size;
    float variance = sum - powf(u, 2.0f);
    return variance;
}

/// returns the covariance of X and Y
float cov(float* x, float* y, int size) {
    float sum = 0;
    for (int i = 0; i < size; i++)
        sum += x[i] * y[i];
    sum /= (float)size;
    float covariance = sum - (avg(x, size) * avg(y, size));
    return covariance;
}

/// returns the Pearson correlation coefficient of X and Y
float pearson(float* x, float* y, int size) {
    float numerator = cov(x, y, size);
    float denominator = sqrtf(var(x, size)) * sqrtf(var(y, size));
    return numerator / denominator;
}

/// performs a linear regression and returns the line equation
//Line linear_reg(Point** points, int size) {
//    float* px = new float[size];
//    float* py = new float[size];
//    for (int i = 0; i < size; i++) {
//        px[i] = points[i]->x;
//        py[i] = points[i]->y;
//    }
//    Line l = linear_reg(px, py, size);
//    delete[] px;
//    delete[] py;
//    return l;
//}

/// performs a linear regression and returns the line equation - but get 2 float* instead point**
Line linear_reg(float* x, float* y, int size) {
    float covariance = cov(x, y, size);
    float variance = var(x, size);
    float averageX = avg(x, size);
    float averageY = avg(y, size);
    float a = covariance / variance;
    float b = averageY - (a * averageX);
    Line l(a, b);
    return l;
}

/// returns the deviation between point p and the line equation of the points
//float dev(Point p, Point** points, int size) {
//    Line l = linear_reg(points, size);
//    return dev(p, l);
//}

/// returns the deviation between point p and the line
float dev(Point p, Line l) {
    float yOnLine = (l.a * p.x) + l.b;
    float distance = p.y - yOnLine;
    return fabs(distance);
}
