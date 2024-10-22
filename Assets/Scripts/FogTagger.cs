﻿using UnityEngine;
using System.Collections.Generic;

public class FogTagger: MonoBehaviour {

	public const float IN_SIGHT_RANGE = 2.5f;

	public ControlSurface controlSurface;

	public ControlSpawn controlSpawn;


	void Start() {
	}


	void Update() {
		var particle = controlSpawn.particle;

		// fix for not reusing the particle
		if (particle == null) {
			return;
		}
		var panels = controlSurface.gridPanels;
		Vector3 particleGridCoord = controlSurface.worldPosToGrid (particle.transform.position.x, particle.transform.position.z);

		// update previous visibility
		for (int x = 0; x < panels.GetLength (0); x++) {
			for (int z = 0; z < panels.GetLength (1); z++) {
				var panel = panels [x, z];
				
				if (Vector3.Distance (particleGridCoord, new Vector3 (x, 0, z)) < IN_SIGHT_RANGE )
				{
					panel.FogState = PanelFogState.InSight;
				}
				else if ( panel.FogState == PanelFogState.InSight )
				{
					panel.FogState = PanelFogState.Discovered;
				}
			}
		}
	}
}