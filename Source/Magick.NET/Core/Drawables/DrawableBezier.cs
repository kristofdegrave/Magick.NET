//=================================================================================================
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

using System.Collections.Generic;

namespace ImageMagick
{
  /// <summary>
  /// Draws a bezier curve through a set of points on the image.
  /// </summary>
  public sealed class DrawableBezier : IDrawable
  {
    private PointDCoordinates _Coordinates;

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableBezier"/> class.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    public DrawableBezier(params PointD[] coordinates)
    {
      _Coordinates = new PointDCoordinates(coordinates, 3);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableBezier"/> class.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    public DrawableBezier(IEnumerable<PointD> coordinates)
    {
      _Coordinates = new PointDCoordinates(coordinates, 3);
    }

    /// <summary>
    /// Gets the coordinates.
    /// </summary>
    public IEnumerable<PointD> Coordinates
    {
      get
      {
        return _Coordinates.ToList();
      }
    }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.Bezier(_Coordinates.ToList());
    }
  }
}