﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C64MemoryModel
{
    public class SimpleUppercaseCharacterSet : CharacterSetBase
    {
        public SimpleUppercaseCharacterSet() : base("simpleUppercase")
        {
            var chars = new Character[] {
                new Character(32, ' '),
                new Character(33, '!'),
                new Character(44, ','),
                new Character(45, '-'),
                new Character(46, '.'),
                new Character(48, '0'),
                new Character(49, '1'),
                new Character(50, '2'),
                new Character(51, '3'),
                new Character(52, '4'),
                new Character(53, '5'),
                new Character(54, '6'),
                new Character(55, '7'),
                new Character(56, '8'),
                new Character(57, '9'),
                new Character(63, '?'),
                new Character(64, '@'),
                new Character(65, 'A'),
                new Character(65, 'a'),
                new Character(66, 'B'),
                new Character(66, 'b'),
                new Character(67, 'C'),
                new Character(67, 'c'),
                new Character(68, 'D'),
                new Character(68, 'd'),
                new Character(69, 'E'),
                new Character(69, 'e'),
                new Character(70, 'F'),
                new Character(70, 'f'),
                new Character(71, 'G'),
                new Character(71, 'g'),
                new Character(72, 'H'),
                new Character(72, 'h'),
                new Character(73, 'I'),
                new Character(73, 'i'),
                new Character(74, 'J'),
                new Character(74, 'j'),
                new Character(75, 'K'),
                new Character(75, 'k'),
                new Character(76, 'L'),
                new Character(76, 'l'),
                new Character(77, 'M'),
                new Character(77, 'm'),
                new Character(78, 'N'),
                new Character(78, 'n'),
                new Character(79, 'O'),
                new Character(79, 'o'),
                new Character(80, 'P'),
                new Character(80, 'p'),
                new Character(81, 'Q'),
                new Character(81, 'q'),
                new Character(82, 'R'),
                new Character(82, 'r'),
                new Character(83, 'S'),
                new Character(83, 's'),
                new Character(84, 'T'),
                new Character(84, 't'),
                new Character(85, 'U'),
                new Character(85, 'u'),
                new Character(86, 'V'),
                new Character(86, 'v'),
                new Character(87, 'W'),
                new Character(87, 'w'),
                new Character(88, 'X'),
                new Character(88, 'x'),
                new Character(89, 'Y'),
                new Character(89, 'y'),
                new Character(90, 'Z'),
                new Character(90, 'z')
            };
            Characters.AddRange(chars);
        }
    }
}