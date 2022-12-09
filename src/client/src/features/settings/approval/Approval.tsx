import { Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { ApprovalPlaceholder } from "features/settings/approval/placeholder/ApprovalPlaceholder";
import { ApprovalCard } from "features/settings/common/approval-card/ApprovalCard";
import { SettingsSection } from "features/settings/common/settings-section/SettingsSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { useQueryClient } from "@tanstack/react-query";
import { approvalKeys, useGetApprovals } from "external/sources/approval/approval.queries";
import { ApprovalDataCm, State } from "@mimirorg/typelibrary-types";
import { usePatchNodeStateReject } from "external/sources/node/node.queries";
import { usePatchTerminalStateReject } from "external/sources/terminal/terminal.queries";
import { usePatchTransportStateReject } from "external/sources/transport/transport.queries";
import { usePatchInterfaceStateReject } from "external/sources/interface/interface.queries";

export const Approval = () => {
  const queryClient = useQueryClient();
  const theme = useTheme();
  const { t } = useTranslation("settings");
  const approvals = useGetApprovals();
  const patcMutationRejectNode = usePatchNodeStateReject();
  const patcMutationRejectTerminal = usePatchTerminalStateReject();
  const patcMutationRejectTransport = usePatchTransportStateReject();
  const patcMutationRejectInterface = usePatchInterfaceStateReject();
  const showPlaceholder = approvals?.data && approvals.data.length === 0;

  const onSubmit = () => {
    setTimeout(() => {
      queryClient.invalidateQueries(approvalKeys.lists());
    }, 500);
  };

  const onReject = (id: string, state: State, objectType: string) => {
    const data: ApprovalDataCm = { id: id, state: state };

    switch (objectType) {
      case "Node":
        patcMutationRejectNode.mutateAsync(data);
        break;
      case "Terminal":
        patcMutationRejectTerminal.mutateAsync(data);
        break;
      case "Interface":
        patcMutationRejectInterface.mutateAsync(data);
        break;
      case "Transport":
        patcMutationRejectTransport.mutateAsync(data);
        break;
    }

    setTimeout(() => {
      queryClient.invalidateQueries(approvalKeys.lists());
    }, 500);
  };

  return (
    <SettingsSection title={t("approval.title")}>
      <Text variant={"title-medium"} mb={theme.tyle.spacing.l}>
        {t("approval.subtitle")}
      </Text>
      <Flexbox flexDirection={"row"} flexWrap={"wrap"} gap={theme.tyle.spacing.xxxl}>
        {showPlaceholder && <ApprovalPlaceholder text={t("approval.placeholders.emptyApproval")} />}
        {approvals.data?.map((approval) => (
          <ApprovalCard key={approval.id} item={approval} onSubmit={onSubmit} onReject={onReject} />
        ))}
      </Flexbox>
    </SettingsSection>
  );
};
