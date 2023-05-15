import { useTheme } from "styled-components";
import { useTranslation } from "react-i18next";
import { useFormContext } from "react-hook-form";
import { Flexbox } from "../../../complib/layouts";
import { FormField } from "../../../complib/form";
import { Input, Textarea } from "../../../complib/inputs";
import { PlainLink } from "../../common/plain-link";
import { Button } from "../../../complib/buttons";
import { UnitLibAm } from "@mimirorg/typelibrary-types";

interface UnitFormBaseFieldsProps {
  limit?: boolean;
}

export default function UnitFormBaseFields({ limit } : UnitFormBaseFieldsProps) {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { register, formState } = useFormContext<UnitLibAm>();
  const { errors } = formState;

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
      <FormField label={t("unit.name")} error={errors.name}>
        <Input placeholder={t("unit.placeholders.name")} {...register("name")} required disabled={limit} />
      </FormField>

      <FormField label={t("unit.symbol")} error={errors.symbol}>
        <Input placeholder={t("unit.placeholders.symbol")} {...register("symbol")} disabled={limit} />
      </FormField>

      <FormField label={t("unit.description")} error={errors.description}>
        <Textarea placeholder={t("unit.placeholders.description")} {...register("description")} />
      </FormField>

      <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"} dangerousAction>
            {t("common.cancel")}
          </Button>
        </PlainLink>
        <Button type={"submit"}>{t("common.submit")}</Button>
      </Flexbox>
    </Flexbox>
  );
}
