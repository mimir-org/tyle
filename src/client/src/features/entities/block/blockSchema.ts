import { YupShape } from "common/types/yupShape";
import { TFunction } from "i18next";
import * as yup from "yup";
import { FormBlockLib } from "./types/formBlockLib";

export const blockSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormBlockLib>>({
    name: yup.string().max(120, t("common.validation.name.max")).required(t("common.validation.name.required")),
    typeReference: yup.string().max(255).nullable(),
    version: yup.string().max(7),
    companyId: yup
      .number()
      .min(1, t("block.validation.companyId.min"))
      .required(t("block.validation.companyId.required")),
    aspect: yup.number().required(t("block.validation.aspect.required")),
    purposeName: yup.string().max(127).required(t("block.validation.purposeName.required")),
    rdsId: yup.string().required(t("block.validation.rdsId.required")),
    symbol: yup.string().max(127).nullable(),
    description: yup.string().max(500, t("common.validation.description.max")).nullable(),
    blockTerminals: yup
      .array()
      .of(
        yup.object().shape({
          terminalId: yup.string().required(t("block.validation.blockTerminals.terminalId.required")),
          connectorDirection: yup
            .number()
            .required(t("block.validation.blockTerminals.direction.required")),
          maxQuantity: yup
            .number()
            .min(0, t("block.validation.blockTerminals.maxQuantity.min"))
            .required(),
          minQuantity: yup
            .number()
            .min(0, t("block.validation.blockTerminals.minQuantity.min"))
            .required(),
        }),
      )
      .test("Uniqueness", t("block.validation.blockTerminals.array.unique"), (terminals) => {
        const uniqueTerminalAndDirectionCombinations = new Set(
          terminals?.map((x) => `${x.terminalId}${x.connectorDirection}`),
        );
        return terminals?.length === uniqueTerminalAndDirectionCombinations.size;
      }),
    attributes: yup.array().nullable(),
  });
