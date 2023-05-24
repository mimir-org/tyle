import { UnitLibAm } from "@mimirorg/typelibrary-types";
import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";

export const unitSchema = (t: TFunction<"translation">) => 
  yup.object<YupShape<UnitLibAm>>({
  name: yup.string().max(120, t("unit.validation.name.max")).required(),
  description: yup.string().max(500).required(),
  typeReference: yup.string(),
  symbol: yup.string().max(30, t("unit.validation.symbol.max")).required(),
});
