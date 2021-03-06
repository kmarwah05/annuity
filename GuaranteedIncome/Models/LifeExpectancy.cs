﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedIncome.Models
{
    public class LifeExpectancy
    {
       public static (double age, double lifeExpectancy)[] GenderLifeExpectancy(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return new(double age, double lifeExpectancy)[]
                    {
                       (0, 76.15), (1, 75.63),(2, 74.67),(3, 73.69),(4, 72.71),(5, 71.72),(6, 70.73), (7, 69.74), (8, 68.75), (9, 67.76), (10, 66.76),
                       (11, 65.77), (12, 64.78), (13, 63.79), (14, 62.8), (15, 61.82), (16, 60.84), (17, 59.88),
                       (18, 58.91), (19, 57.96), (20, 57.01), (21, 56.08), (22, 55.14), (23, 54.22), (24, 53.29), (25, 52.37), (26, 51.44), (27, 50.52), (28, 49.59),
                       (29, 48.67), (30, 47.75),(31, 46.82), (32, 45.9), (33, 44.98), (34, 44.06), (35, 43.14), (36, 42.22), (37, 41.3), (38, 40.38), (39, 39.46), (40, 38.54),
                       (41, 37.63), (42, 36.72), (43, 35.81), (44, 34.9),(45, 34), (46, 33.11), (47, 32.22), (48, 31.34), (49, 30.46), (50, 29.6), (51, 28.75), (52, 27.9),
                       (53, 27.07), (54, 26.25), (55, 25.43), (56, 24.63), (57, 23.83), (58, 23.05), (59, 22.27), (60, 21.51), (61, 20.75), (62, 20), (63, 19.27), (64, 18.53),
                       (65, 17.81), (66, 17.09), (67, 16.38), (68, 15.68), (69, 14.98), (70, 14.3), (71, 13.63), (72, 12.97), (73, 12.33), (74, 11.7), (75, 11.08), (76, 10.48),
                       (77, 9.89), (78, 9.33), (79, 8.77), (80, 8.24), (81, 7.72), (82, 7.23), (83, 6.75), (84, 6.3), (85, 5.87), (86, 5.45), (87, 5.06), (88, 4.69), (89, 4.35),
                       (90, 4.03), (91, 3.73), (92, 3.46), (93, 3.21), (94, 2.99), (95, 2.8), (96, 2.63), (97, 2.48), (98, 2.34), (99, 2.22), (100, 2.11), (101, 2), (102, 1.89),
                       (103, 1.79), (104, 1.69), (105, 1.59), (106, 1.5), (107, 1.41), (108, 1.33), (109, 1.25), (110, 1.17), (111, 1.1), (112, 1.03), (113, 0.96), (114, 0.89), (115, 0.83),
                       (116, 0.77), (117, 0.71), (118, 0.66), (119, 0.61)
                    };
                case Gender.Female:
                    return new(double age, double lifeExpectancy)[]
                    {
                       (0, 80.97),(1, 80.41),(2, 79.44),(3,78.45),(4, 77.47),(5, 76.48),(6, 75.48),(7, 74.49),(8, 73.5),(9, 72.51),(10, 71.51),(11, 70.52), (12, 69.53),
                       (13, 68.53), (14, 67.54), (15, 66.56), (16, 65.57), (17, 64.59), (18, 63.61), (19, 62.63), (20, 61.65), (21, 60.67), (22, 59.7),
                       (23, 58.73), (24, 57.76), (25, 56.79), (26, 55.82), (27, 54.85), (28, 53.88), (29, 52.92), (30, 51.95), (31, 50.99), (32, 50.03), (33, 49.07), (34, 48.11),
                       (35, 47.16),(36, 46.2), (37, 45.25), (38, 44.3), (39, 43.35), (40, 42.41), (41, 41.46), (42, 40.52), (43, 39.59), (44, 38.68), (45, 37.72), (46, 36.8),
                       (47, 35.88), (48, 34.96), (49, 34.06), (50, 33.15), (51, 32.26), (52, 31.37), (53, 30.49), (54, 29.61), (55, 28.74), (56, 27.88), (57, 27.02), (58, 26.17),
                       (59, 25.32), (60, 24.48), (61, 23.64), (62, 22.81), (63, 21.99), (64, 21.17), (65, 20.36), (66, 19.55), (67, 18.76), (68, 17.98), (69, 17.2), (70, 16.44),
                       (71, 15.69), (72, 14.96), (73, 14.24), (74, 13.54), (75, 12.85), (76, 12.17), (77, 11.51), (78, 10.86), (79, 10.24), (80, 9.63), (81, 9.04), (82, 8.48),
                       (83, 7.93), (84, 7.41), (85, 6.91), (86, 6.43), (87, 5.98), (88, 5.54), (89, 5.14), (90, 4.76), (91, 4.41), (92, 4.09), (93, 3.8), (94, 3.54), (95, 3.3),
                       (96, 3.09), (97, 2.9), (98, 2.73), (99, 2.57), (100, 2.42), (101, 2.27), (102, 2.14), (103, 2), (104, 1.88), (105, 1.76), (106, 1.64), (107, 1.53),
                       (108, 1.43), (109, 1.33), (110, 1.24), (111, 1.15), (112, 1.06), (113, 0.98), (114, 0.9), (115, 0.83), (116, 0.77), (117,0.71), (118,0.66), (119,0.61)
                    };
                default:
                    Console.WriteLine("Invalid Life Expectancy Age");
                    return null;
            }    
        }
    }
}
