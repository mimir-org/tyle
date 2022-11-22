import { Dd, Dl, Dt } from "features/settings/access/card/details/AccessCardDetails.styled";
import { Fragment } from "react";

interface AccessCardDetailsProps {
  descriptors: { [key: string]: string };
}

export const AccessCardDetails = ({ descriptors }: AccessCardDetailsProps) => {
  return (
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
};
