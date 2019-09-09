import { Component, OnInit } from '@angular/core';
import { RaceService } from 'src/services/race.service';
import { PilotRaceStatsModel } from 'src/models/PilotRaceStatsModel';
import { LapRaceModel } from 'src/models/LapRaceModel';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  public pilotStats: PilotRaceStatsModel[];

  public bestLapRace: LapRaceModel;

  public displayedColumns: string[] = [
    'position',
    'pilotId',
    'name',
    'meanVelocity',
    'totalRaceTime',
    'bestLapRace',
    'timeAfterWinner',
  ];

  constructor(private raceService: RaceService) {}

  ngOnInit(): void {
    this.raceService.getRace('1').subscribe(res => {
      console.log(res);
      console.log(res.bestLapRace);
      console.table(res.pilotStats);
      this.pilotStats = res.pilotStats;
      this.bestLapRace = res.bestLapRace;
    });
  }
}
