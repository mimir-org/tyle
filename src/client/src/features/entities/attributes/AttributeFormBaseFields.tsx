import { AttributeLibAm, UnitLibCm } from "@mimirorg/typelibrary-types";
import { Button } from "complib/buttons";
import { FormField } from "complib/form";
import { Input, Select, Textarea } from "complib/inputs";
import { Flexbox } from "complib/layouts";
import { PlainLink } from "features/common/plain-link";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { AttributeFormContainer } from "./AttributeFormContainer.styled";
import { useGetUnits } from "../../../external/sources/unit/unit.queries";
import FormUnitSelector from "../../../complib/form/FormUnitSelector";
import { useState } from "react";

/**
 * Component which contains all simple value fields of the attribute form.
 *
 * @constructor
 */
export const AttributeFormBaseFields = () => {
  const [defaultUnit, setDefaultUnit] = useState<UnitLibCm | null>(null);
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, formState } = useFormContext<AttributeLibAm>();
  const { errors } = formState;

  const unitQuery = useGetUnits();
  const units = unitQuery.data || [];

  return (
    <>
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("attribute.name")} error={errors.name}>
          <Input placeholder={t("attribute.placeholders.name")} {...register("name")} disabled={false} />
        </FormField>

        <FormField label={t("attribute.description")} error={errors.description}>
          <Textarea placeholder={t("attribute.placeholders.description")} {...register("description")} />
        </FormField>

        <FormField label={t("attributeUnits")} error={errors.attributeUnits}>
          <Controller
            control={control}
            name={"attributeUnits"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                placeholder={t("common.templates.select", { object: t("unitId") })}
                options={units}
                isLoading={unitQuery.isLoading}
                selectRef={ref}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.id.toString()}
                onChange={(x) => {
                  onChange([{unitId: x?.id, isDefault: true}]);
                  setDefaultUnit(x);
                }}
              />
            )}
          />
        </FormField>

        <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
          <PlainLink tabIndex={-1} to={"/"}>
            <Button tabIndex={0} as={"span"} variant={"outlined"} dangerousAction>
              {t("common.cancel")}
            </Button>
          </PlainLink>
          <Button type={"submit"}>{t("common.submit")}</Button>
        </Flexbox>
        <FormUnitSelector units={units.filter((unit) => unit.id === defaultUnit?.id)} />
      </Flexbox>
    </>
  );
};
