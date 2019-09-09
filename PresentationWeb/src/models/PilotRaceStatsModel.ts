export class PilotRaceStatsModel {
  public pilotId: string;

  public name: string;

  public position: number;

  public meanVelocity: number;

  public totalRaceTime: string; // only display there's no operation

  public lapRaceQuantity: number;

  public bestLapRace: number;

  public timeAfterWinner: string; // only display there's no operation
}
