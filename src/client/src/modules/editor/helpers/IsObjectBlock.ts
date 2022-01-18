import { ObjectType } from "../../../models";

const IsObjectBlock = (object: ObjectType) => {
  return object === ObjectType.ObjectBlock;
};

export default IsObjectBlock;
