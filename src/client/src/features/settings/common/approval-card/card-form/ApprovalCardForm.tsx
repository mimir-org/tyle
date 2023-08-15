import { yupResolver } from "@hookform/resolvers/yup";
import { ApprovalCm, State } from "@mimirorg/typelibrary-types";
import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
import { Form } from "complib/form";
import { Button, Flexbox, Input, Text } from "@mimirorg/component-library";
import { useForm } from "react-hook-form";
import { useTranslation } from "react-i18next";
import {
  useApprovalToasts,
  findNextState,
} from "features/settings/common/approval-card/card-form/ApprovalCardForm.helpers";
import { approvalSchema } from "features/settings/common/approval-card/card-form/approvalSchema";
import { FormApproval } from "features/settings/common/approval-card/card-form/types/formApproval";
import { theme } from "../../../../../complib/core";

export interface ApprovalCardFormProps {
  item: ApprovalCm;
  formId?: string;
  onSubmit?: () => void;
  onReject?: (id: string, objectType: string) => void;
  showSubmitButton?: boolean;
}

export const ApprovalCardForm = ({
  item,
  formId,
  onSubmit,
  onReject,
  showSubmitButton = true,
}: ApprovalCardFormProps) => {
  const { t } = useTranslation(["settings"]);

  const stateOptions = getOptionsFromEnum<State>(State);
  const nextState = findNextState(item.state);
  const currentState = stateOptions.find((x) => x.value === nextState);

  const { register, handleSubmit } = useForm<FormApproval>({
    resolver: yupResolver(approvalSchema(t)),
    defaultValues: {
      id: item.id,
      objectType: item.objectType,
      state: currentState ?? stateOptions[0],
      companyId: item.companyId,
    },
  });

  const toast = useApprovalToasts();

  return (
    <Form id={formId} onSubmit={handleSubmit((data) => toast(item.id, data).then(() => onSubmit && onSubmit()))}>
      <Flexbox flexFlow={"row"} justifyContent={"space-between"} style={{ marginTop: "8px" }}>
        <Input type={"hidden"} value={item.id} {...register("id")} />
        <Input type={"hidden"} value={item.objectType} {...register("objectType")} />
        <Input type={"hidden"} value={item.companyId} {...register("companyId")} />
        <Input type={"hidden"} value={nextState} {...register("state")} />
        <Text variant={"body-large"}>{`Requesting to be ${stateOptions[nextState].label.toLowerCase()}`}</Text>
        <Flexbox justifyContent={"center"} alignItems={"center"} flexFlow="row" gap={theme.spacing.base}>
          {onReject && (
            <>
              {showSubmitButton && (
                <Button dangerousAction type={"button"} onClick={() => onReject(item.id, item.objectType)}>
                  {t("common.approval.reject")}
                </Button>
              )}
            </>
          )}
          {showSubmitButton && <Button type={"submit"}>{t("common.approval.submit")}</Button>}
        </Flexbox>
      </Flexbox>
    </Form>
  );
};
