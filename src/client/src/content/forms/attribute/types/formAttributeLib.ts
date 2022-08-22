import { AttributeLibAm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "../../../../data/types/updateEntity";
import { ValueObject } from "../../types/valueObject";

export interface FormAttributeLib extends Omit<UpdateEntity<AttributeLibAm>, "unitIdList"> {
  unitIdList: ValueObject<string>[];
}