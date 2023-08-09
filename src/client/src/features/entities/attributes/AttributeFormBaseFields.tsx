import { FormField } from "complib/form";
import { Input, Select, Textarea } from "@mimirorg/component-library";
import { Controller, useFormContext, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useGetUnits } from "../../../external/sources/unit/unit.queries";
import { FormAttributeLib, toFormUnitHelper } from "./types/formAttributeLib";
import { FormBaseFieldsContainer } from "../../../complib/form/FormContainer.styled";

interface AttributeFormBaseFieldsProps {
  limited?: boolean;
}

/**
 * Component which contains all simple value fields of the attribute form.
 *
 * @constructor
 */

export const AttributeFormBaseFields = ({ limited }: AttributeFormBaseFieldsProps) => {
  const { t } = useTranslation("entities");
  const {
    control,
    register,
    setValue,
    formState: { errors },
  } = useFormContext<FormAttributeLib>();

  const unitQuery = useGetUnits();
  const units = unitQuery.data || [];
  const chosenUnits = useWatch({ control, name: "units" });
  const defaultUnit = useWatch({ control, name: "defaultUnit" });

  return (
    <FormBaseFieldsContainer>
      <FormField label={t("attribute.name")} error={errors.name}>
        <Input placeholder={t("attribute.placeholders.name")} {...register("name")} disabled={limited} />
      </FormField>

      <FormField label={t("unit.multiple")} error={errors.units}>
        <Controller
          name="units"
          control={control}
          render={({ field: { onChange, ref, ...rest } }) => (
            <Select
              {...rest}
              selectRef={ref}
              placeholder={t("common.templates.select", { object: t("unit.multiple").toLowerCase() })}
              options={units.map((x) => toFormUnitHelper(x))}
              isLoading={unitQuery.isLoading}
              getOptionLabel={(x) => x.name}
              getOptionValue={(x) => x.unitId}
              isMulti={true}
              isDisabled={limited}
              onChange={(val) => {
                if (val.filter((x) => x.unitId === defaultUnit?.unitId).length === 0) {
                  if (val.length === 0) setValue("defaultUnit", null);
                  else setValue("defaultUnit", val[0]);
                }
                onChange(val);
              }}
            />
          )}
        />
      </FormField>

      {chosenUnits.length > 0 && (
        <FormField label={t("unit.defaultUnitTitle")} error={errors.defaultUnit}>
          <Controller
            name="defaultUnit"
            control={control}
            render={({ field: { ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("unit.defaultUnitTitle").toLowerCase() })}
                options={chosenUnits}
                isLoading={unitQuery.isLoading}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.unitId}
                defaultValue={chosenUnits[0]}
                isDisabled={limited}
              />
            )}
          />
        </FormField>
      )}

      <FormField label={t("attribute.description")} error={errors.description}>
        <Textarea placeholder={t("attribute.placeholders.description")} {...register("description")} />
      </FormField>
    </FormBaseFieldsContainer>
  );
};
