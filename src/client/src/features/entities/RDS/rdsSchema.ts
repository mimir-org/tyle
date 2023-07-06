import { RdsLibAm } from "@mimirorg/typelibrary-types";
import { YupShape } from "common/types/yupShape";
import { TFunction } from "i18next";
import * as yup from "yup";

export const rdsSchema = (t: TFunction<"translation">, usedRdsCodes: string[]) =>
  yup.object<YupShape<RdsLibAm>>({
    rdsCode: yup
      .string()
      .max(10, t("rds.validation.rdsCode.max"))
      .required(t("rds.validation.rdsCode.required"))
      .test(
        "uniqueRdsCheck",
        t("rds.validation.rdsCode.unique"),
        (value) => !usedRdsCodes.includes(value.toUpperCase()),
      ),
    name: yup.string().max(120, t("common.validation.name.max")).required(t("common.validation.name.required")),
    typeReference: yup.string().max(255).nullable(),
    description: yup.string().max(500, t("common.validation.description.max")).nullable(),
  });
