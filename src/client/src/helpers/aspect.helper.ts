import { Aspect } from "common/types/common/aspect";

export const getColorFromAspect = (aspect?: Aspect) => {
  switch (aspect) {
    case Aspect.Function:
      return "hsl(57,99%,63%)";
    case Aspect.Product:
      return "hsl(184,100%,50%)";
    case Aspect.Location:
      return "hsl(299,100%,50%)";
    default:
      return "hsl(318, 29%, 91%)";
  }
};
