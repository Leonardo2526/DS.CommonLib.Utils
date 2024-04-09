namespace DS.ClassLib.VarUtils.BinaryOperations
{
    /// <summary>
    /// Base binary operations between A and B sets.
    /// </summary>
    public enum BinaryOperationType
    {
        /// <summary>
        /// The set of all things that are members of A or B or both.
        /// </summary>
        Union,

        /// <summary>
        /// The set of all things that are members of both A and B.
        /// </summary>
        Intersection,

        /// <summary>
        /// The set of all things that belong to A but not B.
        /// </summary>
        Difference,

        /// <summary>
        /// The set of all things that belong to A or B but not both.
        /// </summary>
        SymmetricDifference,

        /// <summary>
        /// The set of all elements (of U) that do not belong to A.
        /// </summary>
        Complement
    }
}
