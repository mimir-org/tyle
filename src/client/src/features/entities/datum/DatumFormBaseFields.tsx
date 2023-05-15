import { FormField } from "../../../complib/form";
import { Input, Select, Textarea } from "../../../complib/inputs";
import { Controller, useFormContext } from "react-hook-form";
import { QuantityDatumLibAm, QuantityDatumType } from "@mimirorg/typelibrary-types";
import { useTranslation } from "react-i18next";

export const DatumFormBaseFields = () => {
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
      <FormField label={t("datum.name")} error={errors.name}>
        <Input placeholder={t("datum.placeholders.name")} {...register("name")} required />
      </FormField>

      <FormField label={t("datum.description")} error={errors.description}>
        <Textarea placeholder={t("datum.placeholders.description")} {...register("description")} />
      </FormField>

      <FormField label={t("datum.quantityType")} error={errors.quantityDatumType}>
        <Controller
          control={control}
          name={"quantityDatumType"}
          render={({ field: { ref, onChange } }) => (
            <Select
              required={true}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("datum.title").toLowerCase() })}
              options={quantityDatumTypeArray}
              getOptionValue={(x) => x.value}
              getOptionLabel={(x) => x.name}
              defaultValue={quantityDatumTypeArray[0]}
              onChange={(x) => {
                onChange(x?.value);
              }}
            />
          )}
        />
      </FormField>
    </>
  );
};
