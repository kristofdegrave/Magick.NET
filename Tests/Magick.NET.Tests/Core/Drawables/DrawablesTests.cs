﻿//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace Magick.NET.Tests
{
  [TestClass]
  public class DrawablesTests
  {
    [TestMethod]
    public void Test_Draw()
    {
      using (MagickImage image = new MagickImage(MagickColors.Fuchsia, 100, 100))
      {
        Drawables drawables = new Drawables()
          .FillColor(MagickColors.Red)
          .Rectangle(10, 10, 90, 90);

        IEnumerator enumerator = ((IEnumerable)drawables).GetEnumerator();
        Assert.IsTrue(enumerator.MoveNext());

        drawables.Draw(image);

        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 9, 9);
        ColorAssert.AreEqual(MagickColors.Red, image, 10, 10);
        ColorAssert.AreEqual(MagickColors.Red, image, 90, 90);
        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 91, 91);

        image.Draw(new Drawables()
          .FillColor(MagickColors.Green)
          .Rectangle(15, 15, 85, 85));

        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 9, 9);
        ColorAssert.AreEqual(MagickColors.Red, image, 10, 10);
        ColorAssert.AreEqual(MagickColors.Green, image, 15, 15);
        ColorAssert.AreEqual(MagickColors.Green, image, 85, 85);
        ColorAssert.AreEqual(MagickColors.Red, image, 90, 90);
        ColorAssert.AreEqual(MagickColors.Fuchsia, image, 91, 91);
      }
    }

    [TestMethod]
    public void Test_Exceptions()
    {
      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new Drawables().Draw(null);
      });
    }
  }
}