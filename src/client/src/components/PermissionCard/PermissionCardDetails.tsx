import { Dd, Dl, Dt } from "components/PermissionCard/PermissionCardDetails.styled";
import { Fragment } from "react";

interface PermissionCardDetailsProps {
  descriptors: { [key: string]: string };
}

export const PermissionCardDetails = ({ descriptors }: PermissionCardDetailsProps) => (
  <Dl>
    {descriptors &&
      Object.keys(descriptors).map((k, i) => (
        <Fragment key={i + k}>
          <Dt>{k}</Dt>
          <Dd>{descriptors[k]}</Dd>
        </Fragment>
      ))}
  </Dl>
);
