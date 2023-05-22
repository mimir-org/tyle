import { FormField } from "../../../complib/form";
import { Input, Select, Textarea } from "../../../complib/inputs";
import { Controller, useFormContext } from "react-hook-form";
import { QuantityDatumLibAm, QuantityDatumType } from "@mimirorg/typelibrary-types";
import { useTranslation } from "react-i18next";

interface QuantityDatumFormBaseFieldsProps {
  limit?: boolean;
}

export const QuantityDatumFormBaseFields = ({ limit }: QuantityDatumFormBaseFieldsProps) => {
  const { control, register, formState } = useFormContext<QuantityDatumLibAm>();
  const { errors } = formState;
  const { t } = useTranslation("entities");

  const quantityDatumTypeArray = [
    { name: "Specified scope", value: QuantityDatumType.QuantityDatumSpecifiedScope.toString() },
    { name: "Specified provenance", value: QuantityDatumType.QuantityDatumSpecifiedProvenance.toString() },
    { name: "Specified range", value: QuantityDatumType.QuantityDatumRangeSpecifying.toString() },
    { name: "Specified regularity", value: QuantityDatumType.QuantityDatumRegularitySpecified.toString() },
  ];

  return (
    <>
      <FormField label={t("quantityDatum.name")} error={errors.name}>
        <Input placeholder={t("quantityDatum.placeholders.name")} {...register("name")} required disabled={limit} />
      </FormField>

      <FormField label={t("quantityDatum.description")} error={errors.description}>
        <Textarea placeholder={t("quantityDatum.placeholders.description")} {...register("description")} />
      </FormField>

      <FormField label={t("quantityDatum.quantityType")} error={errors.quantityDatumType}>
        <Controller
          control={control}
          name={"quantityDatumType"}
          render={({ field: { ref, onChange } }) => (
            <Select
              required={true}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("quantityDatum.title").toLowerCase() })}
              options={quantityDatumTypeArray}
              getOptionValue={(x) => x.value}
              getOptionLabel={(x) => x.name}
              defaultValue={quantityDatumTypeArray[0]}
              onChange={(x) => {
                onChange(x?.value);
              }}
              isDisabled={limit}
            />
          )}
        />
      </FormField>
    </>
  );
};
