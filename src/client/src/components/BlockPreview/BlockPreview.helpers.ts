import { Direction } from "types/terminals/direction";

export const meetsInputCriteria = (direction: Direction) => {
  return direction === Direction.Input || direction === Direction.Bidirectional;
};

export const meetsOutputCriteria = (direction: Direction) => {
  return direction === Direction.Output || direction === Direction.Bidirectional;
};
