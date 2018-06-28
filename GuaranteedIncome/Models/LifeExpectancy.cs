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
                       (6, 65), (7,64.1), (8,63.2), (9,62.3), (10,61.4), (11,60.4), (12,59.5), (13,58.6), (14,57.7), (15,56.7), (16, 55.8), (17,54.9),
                       (18, 53.9), (19, 53), (20, 52.1), (21, 51.1), (22, 50.2), (23, 49.3), (24, 48.3), (25, 47.4), (26, 46.5), (27, 45.6), (28, 44.6),
                       (29, 43.7), (30, 42.8),(31, 41.9), (32, 41), (33, 40), (34, 39.1), (35, 38.2), (36, 37.3), (37, 36.5), (38, 35.6), (39, 34.7), (40, 33.8),
                       (41, 33), (42, 32.1), (43, 31.2), (44, 30.4),(45, 29.6), (46, 28.7), (47, 27.9), (48, 27.1), (49, 26.3), (50, 25.5), (51, 24.7), (52, 24),
                       (53, 23.2), (54, 22.4), (55, 21.7), (56, 21), (57, 20.3), (58, 19.6), (59, 18.9), (60, 18.2), (61, 17.5), (62, 16.9), (63, 16.2), (64, 15.6),
                       (65, 15), (66, 14.4), (67, 13.8), (68, 13.2), (69, 12.6), (70, 12.1), (71, 11.6), (72, 11), (73, 10.5), (74, 10.1), (75, 9.6), (76, 9.1),
                       (77, 8.7), (78, 8.3), (79, 7.8), (80, 7.5), (81, 7.1), (82, 6.7), (83, 6.3), (84, 6), (85, 5.7), (86, 5.4), (87, 5.1), (88, 4.8), (89, 4.5),
                       (90, 4.2), (91, 4), (92, 3.7), (93, 3.5), (94, 3.3), (95, 3.1), (96, 2.9), (97, 2.7), (98, 2.5), (99, 2.3), (100, 2.1), (101, 2.1), (102, 1.7),
                       (103, 1.5), (104, 1.3), (105, 1.2), (106, 1), (107, 0.8), (108, 0.7), (109, 0.6), (110, 0.5), (111, 0)
                    };
                case Gender.Female:
                    return new(double age, double lifeExpectancy)[]
                    {
                       (11, 65), (12,64.1), (13,63.2), (14,62.3), (15,61.4), (16,60.4), (17,59.5), (18,58.6), (19,57.7), (20,56.7), (21, 55.8), (22,54.9),
                       (23, 53.9), (24, 53), (25, 52.1), (26, 51.1), (27, 50.2), (28, 49.3), (29, 48.3), (30, 47.4), (31, 46.5), (32, 45.6), (33, 44.6), (34, 43.7),
                       (35, 42.8),(36, 41.9), (37, 41), (38, 40), (39, 39.1), (40, 38.2), (41, 37.3), (42, 36.5), (43, 35.6), (44, 34.7), (45, 33.8), (46, 33),
                       (47, 32.1), (48, 31.2), (49, 30.4), (50, 29.6), (51, 28.7), (52, 27.9), (53, 27.1), (54, 26.3), (55, 25.5), (56, 24.7), (57, 24), (58, 23.2),
                       (59, 22.4), (60, 21.7), (61, 21), (62, 20.3), (63, 19.6), (64, 18.9), (65, 18.2), (66, 17.5), (67, 16.9), (68, 16.2), (69, 15.6), (70, 15),
                       (71, 14.4), (72, 13.8), (73, 13.2), (74, 12.6), (75, 12.1), (76, 11.6), (77, 11), (78, 10.5), (79, 10.1), (80, 9.6), (81, 9.1), (82, 8.7),
                       (83, 8.3), (84, 7.8), (85, 7.5), (86, 7.1), (87, 6.7), (88, 6.3), (89, 6), (90, 5.7), (91, 5.4), (92, 5.1), (93, 4.8), (94, 4.5), (95, 4.2),
                       (96, 4), (97, 3.7), (98, 3.5), (99, 3.3), (100, 3.1), (101, 2.9), (102, 2.7), (103, 2.5), (104, 2.3), (105, 2.1), (106, 2.1), (107, 1.7),
                       (108, 1.5), (109, 1.3), (110, 1.2), (111, 1), (112, 0.8), (113, 0.7), (114, 0.6), (115, 0.5), (116, 0)
                    };
                default:
                    Console.WriteLine("Invalid Life Expectancy Age");
                    return null;
            }
        }
    }
}