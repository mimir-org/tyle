import { YupShape } from "types/yupShape";
import * as yup from "yup";
import { FormAttributeGroupLib } from "./formAttributeGroupLib";

export const attributeGroupSchema = () =>
  yup.object<YupShape<FormAttributeGroupLib>>({
    name: yup.string().max(120, "The name can be at most 120 characters").required("Please enter a name"),
    description: yup.string().max(500, "The description can be at most 500 characters").nullable(),
  });
