import { Button } from "complib/buttons";
import { FormField } from "complib/form";
import { Input, Select, Textarea } from "complib/inputs";
import { Flexbox } from "complib/layouts";
import { PlainLink } from "features/common/plain-link";
import { useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useGetUnits } from "../../../external/sources/unit/unit.queries";
import { useEffect, useState } from "react";
import { FormUnitHelper } from "../units/types/FormUnitHelper";
import { FormAttributeLib } from "./types/formAttributeLib";
import { FormBaseFieldsContainer } from "../../../complib/form/FormContainer.styled";
import { Text } from "../../../complib/text";

interface AttributeFormBaseFieldsProps {
  limited?: boolean;
}

/**
 * Component which contains all simple value fields of the attribute form.
 *
 * @constructor
 */

export const AttributeFormBaseFields = ({ limited }: AttributeFormBaseFieldsProps) => {
  const [unitArray, setUnitArray] = useState<FormUnitHelper[]>([]);
  const [defaultUnit, setDefaultUnit] = useState<FormUnitHelper | null>(null);
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { register, setValue, formState } = useFormContext<FormAttributeLib>();
  const { errors } = formState;

  register("attributeUnits");

  const unitQuery = useGetUnits();
  const units = unitQuery.data || [];

  useEffect(() => {
    setValue("attributeUnits", unitArray);
    unitArray.length > 0 && setDefaultUnit(unitArray.find((unit) => unit.isDefault) || null);
  }, [setValue, unitArray]);

  return (
    <FormBaseFieldsContainer>
      <Text variant={"display-small"}>{t("attribute.title")}</Text>
      <FormField label={t("attribute.name")} error={errors.name}>
        <Input placeholder={t("attribute.placeholders.name")} {...register("name")} disabled={limited} />
      </FormField>

      <FormField label={t("attribute.description")} error={errors.description}>
        <Textarea placeholder={t("attribute.placeholders.description")} {...register("description")} />
      </FormField>

      <FormField label={t("unit.multiple")} error={errors.attributeUnits}>
        <Select
          placeholder={t("common.templates.select", { object: t("unit.defaultUnitTitle").toLowerCase() })}
          options={units}
          isLoading={unitQuery.isLoading}
          getOptionLabel={(x) => x.name}
          getOptionValue={(x) => x.id.toString()}
          isMulti={true}
          onChange={(x) => {
            setUnitArray(
              x.map((unit) => ({
                symbol: unit.symbol,
                name: unit.name,
                unitId: unit.id,
                description: unit.description,
                isDefault: unit.id === defaultUnit?.unitId || unitArray.length === 0,
              }))
            );
            setValue("attributeUnits", unitArray);
          }}
          isDisabled={limited}
        />
      </FormField>
      {unitArray.length > 0 && (
        <FormField label={t("unit.defaultUnitTitle")} error={errors.attributeUnits}>
          <Select
            placeholder={t("common.templates.select", { object: t("unit.defaultUnitTitle").toLowerCase() })}
            options={unitArray}
            isLoading={unitQuery.isLoading}
            getOptionLabel={(x) => x.name}
            getOptionValue={(x) => x.unitId}
            defaultValue={unitArray[0]}
            onChange={(x) => {
              setUnitArray(
                unitArray.map((unit) => {
                  unit.unitId === x?.unitId && setDefaultUnit(unit);
                  return { ...unit, isDefault: unit.unitId === x?.unitId };
                })
              );
            }}
            isDisabled={limited}
          />
        </FormField>
      )}

      <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"} dangerousAction>
            {t("common.cancel")}
          </Button>
        </PlainLink>
        <Button type={"submit"}>{t("common.submit")}</Button>
      </Flexbox>
    </FormBaseFieldsContainer>
  );
};
