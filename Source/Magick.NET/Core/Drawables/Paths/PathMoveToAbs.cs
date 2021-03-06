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

namespace ImageMagick
{
  /// <summary>
  /// Starts a new sub-path at the given coordinate using absolute coordinates. The current point
  /// then becomes the specified coordinate.
  /// </summary>
  public sealed class PathMoveToAbs : IPath
  {
    private PointD _Coordinate;

    /// <summary>
    /// Initializes a new instance of the <see cref="PathMoveToAbs"/> class.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    public PathMoveToAbs(double x, double y)
     : this(new PointD(x, y))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PathMoveToAbs"/> class.
    /// </summary>
    /// <param name="coordinate">The coordinate to use.</param>
    public PathMoveToAbs(PointD coordinate)
    {
      _Coordinate = coordinate;
    }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IPath.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.PathMoveToAbs(_Coordinate.X, _Coordinate.Y);
    }
  }
}