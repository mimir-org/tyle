import { QuantityDatumLibAm } from "@mimirorg/typelibrary-types";
import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";

export const quantityDatumSchema = (t: TFunction<"translation">) => 
  yup.object<YupShape<QuantityDatumLibAm>>({
  name: yup.string().max(120, t("quantityDatum.validation.name.max")).required(),
  description: yup.string().max(500).required(),
  typeReference: yup.string(),
  quantityDatumType: yup.number().required(t("quantityDatum.validation.type.required"))
});
