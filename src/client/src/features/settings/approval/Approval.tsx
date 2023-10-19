import { Flexbox, Text } from "@mimirorg/component-library";
import { ApprovalPlaceholder } from "features/settings/approval/placeholder/ApprovalPlaceholder";
import { ApprovalCard } from "features/settings/common/approval-card/ApprovalCard";
import { SettingsSection } from "features/settings/common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useQueryClient } from "@tanstack/react-query";
import { approvalKeys, useGetApprovals } from "external/sources/approval/approval.queries";
import { ApprovalDataCm, State } from "@mimirorg/typelibrary-types";

import { usePatchAttributeState } from "../../../external/sources/attribute/attribute.queries";
import { usePatchRdsState } from "../../../external/sources/rds/rds.queries";
import { usePatchQuantityDatumState } from "../../../external/sources/datum/quantityDatum.queries";

export const Approval = () => {
  const queryClient = useQueryClient();
  const theme = useTheme();
  const { t } = useTranslation("settings");
  const approvals = useGetApprovals();
  //  const patchMutationBlock = usePatchBlockState();
  //const patchMutationTerminal = usePatchTerminalState();
  const patchMutationAttribute = usePatchAttributeState();
  const patchMutationQuantityDatum = usePatchQuantityDatumState();
  const patchMutationRds = usePatchRdsState();
  //const patchMutationUnit = usePatchUnitState();
  const showPlaceholder = approvals?.data && approvals.data.length === 0;

  const onSubmit = () => {
    setTimeout(() => {
      queryClient.invalidateQueries(approvalKeys.lists());
    }, 500);
  };

  /*
   * Rejects an approval request
   * @param id the id of the approval request
   * @param state the state to set the approval request to
   * @param objectType the type of object the approval request is for
   * @see State
   */
  const onReject = (id: string, objectType: string) => {
    const data: ApprovalDataCm = { id: id, state: State.Draft };

    switch (objectType) {
      case "Block":
        // patchMutationBlock.mutateAsync(data);
        break;
      case "Terminal":
        //patchMutationTerminal.mutateAsync(data);
        break;
      case "Attribute":
        patchMutationAttribute.mutateAsync(data);
        break;
      case "Unit":
        //patchMutationUnit.mutateAsync(data);
        break;
      case "Quantity datum":
        patchMutationQuantityDatum.mutateAsync(data);
        break;
      case "Rds":
        patchMutationRds.mutateAsync(data);
        break;
      default:
        break;
    }

    setTimeout(() => {
      queryClient.invalidateQueries(approvalKeys.lists());
    }, 500);
  };

  return (
    <SettingsSection title={t("approval.title")}>
      {/* Approval */}
      <Text variant={"title-medium"} spacing={{ mb: theme.mimirorg.spacing.l }}>
        {t("approval.approval")}
      </Text>
      <Flexbox flexDirection={"row"} flexWrap={"wrap"} gap={theme.mimirorg.spacing.xxxl}>
        {showPlaceholder && <ApprovalPlaceholder text={t("approval.placeholders.emptyApproval")} />}
        {approvals.data
          ?.filter((x) => x.state === State.Review)
          .map((approval) => (
            <ApprovalCard key={`${approval.id}`} item={approval} onSubmit={onSubmit} onReject={onReject} />
          ))}
      </Flexbox>
    </SettingsSection>
  );
};
