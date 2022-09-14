# Mixins

## Purpose

This directory houses mixins that implement common and reusable functionality for the purpose of styling components.

Many of the mixins available you will find in the "mixins/props" directory, these implement the interfaces defined in the "complib/props" directory.  
The purpose of these is to aid in creating common interfaces for components that expose any of the properties defined within these interfaces.  
As an example two components "Foo" and "Bar" would expose the same property "maxWidth" by using sizing.ts and sizingMixin.ts.  
By utilizing the same interfaces and mixins we avoid these components using different names for this property e.g. maxW, maximumWidth etc.

## Partial implementations

Should you want to expose only parts of an interface to the consumer, then consider omitting or picking parts of a given interface.

### Example omit

In the following example all properties defined in the Sizing interface except maxWidth and maxHeight property will be available on the OmittingComponent.

```
interface MyOmittingInterface extends Omit<Sizing, "maxWidth" | "maxHeight"> {
    someAdditionalProperty?: string;
}

const OmittingComponent = styled.div<MyOmittingInterface>`
    ${sizingMixin};

    // Some custom property might follow
    color: ${(props) => props.someAdditionalProperty};

    // Other properties that are not exposed to the consumer could also be defined within
    font-size: 32px;
`;
```

### Example pick

In the following example all properties defined in the Typography interface in addition to the maxWidth and maxHeight properties from Sizing will be available on the PickyComponent.

```
interface MyPickyInterface extends Typography, Pick<Sizing, "maxWidth" | "maxHeight"> {
    someAdditionalProperty?: string;
}

const PickyComponent = styled.div<MyPickyInterface>`
    ${typographyMixin};
    ${sizingMixin};

    // Some custom property might follow
    color: ${(props) => props.someAdditionalProperty};

    // Other properties that are not exposed to the consumer could also be defined within
    font-size: 32px;
`;
```
