export interface BorderSystem {
  radius: {
    small: string;
    medium: string;
    large: string;
    round: string;
  };
}

export const border: BorderSystem = {
  radius: {
    small: "3px",
    medium: "5px",
    large: "10px",
    round: "50%",
  },
};
