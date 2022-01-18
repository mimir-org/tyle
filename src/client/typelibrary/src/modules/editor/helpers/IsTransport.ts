import { ObjectType } from "../../../models";

const IsTransport = (object: ObjectType) => {
  return object === ObjectType.Transport;
};

export default IsTransport;
