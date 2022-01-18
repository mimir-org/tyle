import { Aspect, ObjectType } from "../../../models";

const GetObjectTypeDescription = (aspect: Aspect, objectType: ObjectType): string => {
  switch (objectType) {
    case ObjectType.NotSet:
      return "Not set";
    case ObjectType.Interface:
      return "Interface";
    case ObjectType.Transport:
      return "Transport";
    case ObjectType.ObjectBlock:
      if (aspect === Aspect.None || aspect === Aspect.NotSet) {
        return "System Block";
      } else {
        return `${Aspect[aspect]} System Block`;
      }
    default:
      return "Not set";
  }
};

export default GetObjectTypeDescription;
