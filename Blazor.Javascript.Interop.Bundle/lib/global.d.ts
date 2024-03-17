declare const DotNet: {
    invokeMethodAsync<T>(assemblyName: string, methodIdentifier: string, ...args: any[]): Promise<T>
    attachReviver(reviver: (key: string, value: any) => any): void;
    createJSObjectReference(data: Record<string, object>): Record<string, object>;
};