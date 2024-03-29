export interface ShadowSystem {
  small: string;
  medium: string;
  large: string;
  xl: string;
}

export const shadow: ShadowSystem = {
  small: "0px 2px 4px hsla(0, 0%, 0%, 0.15)",
  medium: "0 6px 20px -2px hsla(0, 0%, 0%, 0.2)",
  large: "0 15px 50px -10px hsla(0, 0%, 0%, 0.3)",
  xl: "0 25px 80px -15px hsla(0, 0%, 0%, 0.5)",
};
