import { RdsLibAm } from "@mimirorg/typelibrary-types";
import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";

export const rdsSchema = (t: TFunction<"translation">) => 
  yup.object<YupShape<RdsLibAm>>({
  rdsCode: yup.string().max(10, t("rds.validation.rdsCode.max")),
  name: yup.string().max(120, t("rds.validation.name.max")).required(),
  description: yup.string().max(500).required(),
  typeReference: yup.string(),
});
