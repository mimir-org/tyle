import { useTranslation } from "react-i18next";
import { useFormContext } from "react-hook-form";
import { FormBaseFieldsContainer, FormField, Input, Textarea } from "@mimirorg/component-library";
import { UnitLibAm } from "@mimirorg/typelibrary-types";

interface UnitFormBaseFieldsProps {
  limited?: boolean;
}

export default function UnitFormBaseFields({ limited }: UnitFormBaseFieldsProps) {
  const { t } = useTranslation("entities");
  const { register, formState } = useFormContext<UnitLibAm>();
  const { errors } = formState;

  return (
    <FormBaseFieldsContainer>
      <FormField label={t("unit.name")} error={errors.name}>
        <Input placeholder={t("unit.placeholders.name")} {...register("name")} required disabled={limited} />
      </FormField>

      <FormField label={t("unit.symbol")} error={errors.symbol}>
        <Input placeholder={t("unit.placeholders.symbol")} {...register("symbol")} disabled={limited} />
      </FormField>

      <FormField label={t("unit.description")} error={errors.description}>
        <Textarea placeholder={t("unit.placeholders.description")} {...register("description")} />
      </FormField>
    </FormBaseFieldsContainer>
  );
}
