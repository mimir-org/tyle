import { Dd, Dl, Dt } from "./ApprovalCardDetails.styled";
import { Fragment } from "react";

interface ApprovalCardDetailsProps {
  descriptors: { [key: string]: string };
}

export const ApprovalCardDetails = ({ descriptors }: ApprovalCardDetailsProps) => (
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
