export interface DTransport{
  transportId: number;
  transportName: string;
  transportTypeId?: number;
  transportTypeName?: string;
  transportSeatCapacity?: number;
  transportEngineNo?: string;
  transportRegNo?: string;
  enginePower?: number;
  taxToken?: number;
  fitness?: number;
  ait?: number;
  fitnessExpiry?: Date;
  taxTokenExpiry?: Date;
}