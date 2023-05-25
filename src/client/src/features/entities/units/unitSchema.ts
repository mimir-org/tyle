import { UnitLibAm } from "@mimirorg/typelibrary-types";
import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";

export const unitSchema = (t: TFunction<"translation">) => 
  yup.object<YupShape<UnitLibAm>>({
    name: yup.string().max(120, t("common.validation.name.max")).required(t("common.validation.name.required")),
    typeReference: yup.string().max(255),
    symbol: yup.string().max(30, t("unit.validation.symbol.max")),
    description: yup.string().max(500, t("common.validation.description.max"))
  });
