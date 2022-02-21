import { ObjectType } from "../../../models";

const IsInterface = (object: ObjectType) => {
  return object === ObjectType.Interface;
};

export default IsInterface;
