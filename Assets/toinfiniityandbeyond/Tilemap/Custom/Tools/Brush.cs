﻿using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace toinfiniityandbeyond.Tilemapping
{
	[Serializable]
	public class Brush : ScriptableTool
	{
		/// <summary>
		/// The default constructor where you can set up default variable values
		/// </summary>
		public Brush () : base ()
		{
			radius = 1;
			shape = BrushShape.Square;
		}

		public int radius;
		public enum BrushShape { Square, Circle, }
		public BrushShape shape;

		public override KeyCode Shortcut { get { return KeyCode.B; } }
		public override string Description { get { return "A simple brush"; } }

		/// <summary>
		/// Called by the tilemap to paint tiles
		/// </summary>
		/// <param name="point">Where you want to paint the tile</param>
		/// <param name="tile">The BaseTile you want to paint</param>
		/// <param name="map">What you want to paint on</param>
		public override bool Use (Coordinate point, ScriptableTile tile, TileMap map)
		{
			if (tile == null || map == null)
				return false;

			int correctedRadius = radius - 1;

			bool result = false;
			for (int x = -correctedRadius; x <= correctedRadius; x++)
			{
				for (int y = -correctedRadius; y <= correctedRadius; y++)
				{
					Coordinate offsetPoint = point + new Coordinate (x, y);
			
						if (shape == BrushShape.Circle) {
							Vector2 delta = (Vector2)(offsetPoint - point);
							if (delta.sqrMagnitude > radius * radius)
								continue;
						}

					if (map.SetTileAt (offsetPoint, tile))
					{
						result = true;
					}
				}
			}
			return result;
		}
	}
}