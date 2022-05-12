import { Aspect } from "../models/tyle/enums/aspect";

export const getColorFromAspect = (aspect: Aspect) => {
  switch (aspect) {
    case Aspect.NotSet:
    case Aspect.None:
      return "";
    case Aspect.Function:
      return "#fef445";
    case Aspect.Product:
      return "#00f0ff";
    case Aspect.Location:
      return "#fa00ff";
  }
};
