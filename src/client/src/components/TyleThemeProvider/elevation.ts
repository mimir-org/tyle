interface Elevation {
  opacity: number;
}

export interface ElevationSystem {
  levels: {
    0: Elevation;
    1: Elevation;
    2: Elevation;
    3: Elevation;
    4: Elevation;
    5: Elevation;
  };
}

export const elevation: ElevationSystem = {
  levels: {
    0: {
      opacity: 0,
    },
    1: {
      opacity: 0.05,
    },
    2: {
      opacity: 0.08,
    },
    3: {
      opacity: 0.11,
    },
    4: {
      opacity: 0.12,
    },
    5: {
      opacity: 0.14,
    },
  },
};
