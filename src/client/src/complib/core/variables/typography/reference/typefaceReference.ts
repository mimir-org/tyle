export interface TypefaceReference {
  brand: string,
  weights: {
    bold: number,
    medium: number,
    normal: number,
    light: number,
  }
}

export const typefaceReference: TypefaceReference = {
  brand: "roboto, sans-serif",
  weights: {
    bold: 700,
    medium: 500,
    normal: 400,
    light: 300,
  }
};