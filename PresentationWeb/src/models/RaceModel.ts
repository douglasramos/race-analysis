import { PilotRaceStatsModel } from './PilotRaceStatsModel';
import { LapRaceModel } from './LapRaceModel';

export class RaceModel {
  public pilotStats: PilotRaceStatsModel[];
  public bestLapRace: LapRaceModel;
}
