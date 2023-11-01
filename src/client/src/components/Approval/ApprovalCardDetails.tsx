import { Fragment } from "react";
import { Dd, Dl, Dt } from "./ApprovalCardDetails.styled";

interface ApprovalCardDetailsProps {
  descriptors: { [key: string]: string };
}

const ApprovalCardDetails = ({ descriptors }: ApprovalCardDetailsProps) => (
  <Dl>
    {descriptors &&
      Object.keys(descriptors).map((k, i) => (
        <Fragment key={`${i + k}`}>
          <Dt>{k}</Dt>
          <Dd>{descriptors[k]}</Dd>
        </Fragment>
      ))}
  </Dl>
);

export default ApprovalCardDetails;
