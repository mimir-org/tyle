// import { MimirorgPermission, State } from "@mimirorg/typelibrary-types";
// import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import {
  Button,
  Flexbox,
  FormBaseFieldsContainer,
  FormField,
  Input,
  Select,
  Text,
  Textarea,
  Token,
} from "@mimirorg/component-library";
import { useGetPurposes } from "api/purpose.queries";
// import { useGetAllRds } from "external/sources/rds/rds.queries";
// import { useGetSymbols } from "external/sources/symbol/symbol.queries";
import { BlockFormFields } from "features/entities/block/BlockForm.helpers";
import { Controller, useFormContext, useWatch } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { FormMode } from "../types/formMode";
import { Aspect } from "common/types/common/aspect";
import { PlainLink } from "features/common/plain-link/PlainLink";
import { FormSection } from "../common/form-section/FormSection";
import { SelectItemDialog } from "../common/select-item-dialog/SelectItemDialog";
import { XCircle } from "@styled-icons/heroicons-outline";
import { purposeInfoItem } from "../terminal/TerminalForm.helpers"; //TODO should possibly be moved to own component?

interface BlockFormBaseFieldsProps {
  mode?: FormMode;
  limited?: boolean;
}

/**
 * Component which contains all shared fields for variations of the block form.
 *
 * @param isFirstDraft
 * @param mode
 * @param state
 * @param limited
 * @constructor
 */
export const BlockFormBaseFields = ({ mode, limited }: BlockFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, formState, setValue } = useFormContext<BlockFormFields>();
  const { errors } = formState;

  const purposeQuery = useGetPurposes();
  const purposeInfoItems = purposeQuery.data?.map((p) => purposeInfoItem(p)) ?? [];
  const chosenPurpose = useWatch({ control, name: "purpose" });

  const aspectOptions = getOptionsFromEnum<Aspect>(Aspect);

  return (
    <FormBaseFieldsContainer>
      <Text variant={"display-small"}>{t("block.title")}</Text>

      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l}>
        <FormSection
          title={t("terminal.purpose.title")} //TODO: change name to correct section in langu file.
          action={
            !chosenPurpose && (
              <SelectItemDialog
                title={t("terminal.purpose.dialog.title")}
                description={t("terminal.purpose.dialog.description")}
                searchFieldText={t("terminal.purpose.dialog.search")}
                addItemsButtonText={t("terminal.purpose.dialog.add")}
                openDialogButtonText={t("terminal.purpose.dialog.open")}
                items={purposeInfoItems}
                onAdd={(ids) => {
                  setValue("purpose", purposeQuery.data?.find((x) => x.id === Number(ids[0])));
                }}
                isMultiSelect={false}
              />
            )
          }
        >
          <FormField label={t("terminal.name")} error={errors.symbol}>
            <Input placeholder={t("terminal.placeholders.name")} {...register("name")} disabled={limited} />
          </FormField>

          <Input {...register("purpose")} type="hidden" />
          {chosenPurpose && (
            <Token
              variant={"secondary"}
              actionable
              actionIcon={<XCircle />}
              actionText={t("terminal.purpose.remove")}
              onAction={() => setValue("purpose", undefined)}
              dangerousAction
            >
              {purposeInfoItems.find((x) => x.id === chosenPurpose.id.toString())?.name}
            </Token>
          )}
        </FormSection>

        <FormField label={t("block.aspect")} error={errors.aspect}>
          <Controller
            control={control}
            name={"aspect"}
            render={({ field: { value, onChange, ref, ...rest } }) => (
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("block.aspect").toLowerCase() })}
                options={aspectOptions}
                getOptionLabel={(x) => x.label}
                onChange={(x) => {
                  onChange(x?.value);
                }}
                value={aspectOptions.find((x) => x.value === value)}
              />
            )}
          />
        </FormField>
        <FormField label={t("terminal.symbol")} error={errors.symbol}>
          <Input placeholder={t("terminal.placeholders.symbol")} {...register("symbol")} disabled={limited} />
        </FormField>

        <FormField label={t("block.description")} error={errors.description}>
          <Textarea placeholder={t("block.placeholders.description")} {...register("description")} />
        </FormField>
        <FormField label={t("block.notation")} error={errors.notation}>
          <Textarea placeholder={t("block.placeholders.notation")} {...register("notation")} />
        </FormField>
      </Flexbox>

      <Flexbox justifyContent={"center"} gap={theme.mimirorg.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            {t("common.cancel")}
          </Button>
        </PlainLink>
        <Button type={"submit"}>{mode === "edit" ? t("common.edit") : t("common.submit")}</Button>
      </Flexbox>
    </FormBaseFieldsContainer>
  );
};
