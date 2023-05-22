import { Aspect } from "@mimirorg/typelibrary-types";

export const getColorFromAspect = (aspect: Aspect) => {
  switch (aspect) {
    case Aspect.NotSet:
      return "hsl(318, 29%, 91%)";
    case Aspect.None:
      return "hsl(318, 29%, 91%)";
    case Aspect.Function:
      return "hsl(57,99%,63%)";
    case Aspect.Product:
      return "hsl(184,100%,50%)";
    case Aspect.Location:
      return "hsl(299,100%,50%)";
  }
};
