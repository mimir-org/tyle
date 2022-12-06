import { Dd, Dl, Dt } from "features/settings/common/approval-card/card-details/ApprovalCardDetails.styled";
import { Fragment } from "react";

interface ApprovalCardDetailsProps {
  descriptors: { [key: string]: string };
}

export const ApprovalCardDetails = ({ descriptors }: ApprovalCardDetailsProps) => (
  <Dl>
    {descriptors &&
      Object.keys(descriptors).map((k, i) => (
        <Fragment key={i}>
          <Dt>{k}</Dt>
          <Dd>{descriptors[k]}</Dd>
        </Fragment>
      ))}
  </Dl>
);
