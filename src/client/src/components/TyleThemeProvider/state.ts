export interface StateSystem {
  hover: number;
  focus: number;
  pressed: number;
  dragged: number;
  enabled: number;
  disabled: number;
}

export const state: StateSystem = {
  hover: 0.08,
  focus: 0.12,
  pressed: 0.12,
  dragged: 0.16,
  enabled: 1,
  disabled: 0.12,
};
